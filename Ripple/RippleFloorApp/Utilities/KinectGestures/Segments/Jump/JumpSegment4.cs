﻿using System;
using Microsoft.Kinect;
using System.Diagnostics;


namespace RippleFloorApp.Utilities.KinectGestures.Segments.Jump
{

    class JumpSegment4 : IRelativeGestureSegment
    {
        public static double NewHipCenterAverage = 0.00;
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            double LeftA = Math.Sqrt((Math.Pow(skeleton.Joints[JointType.KneeLeft].Position.Z - skeleton.Joints[JointType.FootLeft].Position.Z, 2) + Math.Pow(skeleton.Joints[JointType.KneeLeft].Position.Y - skeleton.Joints[JointType.FootLeft].Position.Y, 2)));
            double LeftB = Math.Sqrt((Math.Pow(skeleton.Joints[JointType.HipRight].Position.Z - skeleton.Joints[JointType.KneeLeft].Position.Z, 2) + Math.Pow(skeleton.Joints[JointType.HipLeft].Position.Y - skeleton.Joints[JointType.KneeLeft].Position.Y, 2)));
            double LeftC = skeleton.Joints[JointType.HipLeft].Position.Y - skeleton.Joints[JointType.FootLeft].Position.Y;

            double RightA = Math.Sqrt((Math.Pow(skeleton.Joints[JointType.KneeRight].Position.Z - skeleton.Joints[JointType.FootRight].Position.Z, 2) + Math.Pow(skeleton.Joints[JointType.KneeRight].Position.Y - skeleton.Joints[JointType.FootRight].Position.Y, 2)));
            double RightB = Math.Sqrt((Math.Pow(skeleton.Joints[JointType.HipRight].Position.Z - skeleton.Joints[JointType.KneeRight].Position.Z, 2) + Math.Pow(skeleton.Joints[JointType.HipRight].Position.Y - skeleton.Joints[JointType.KneeRight].Position.Y, 2)));
            double RightC = skeleton.Joints[JointType.HipRight].Position.Y - skeleton.Joints[JointType.FootRight].Position.Y;

            double LeftAngle = Math.Acos((Math.Pow(LeftA, 2) + Math.Pow(LeftB, 2) - Math.Pow(LeftC, 2)) / (2 * LeftA * LeftB));
            double RightAngle = Math.Acos((Math.Pow(RightA, 2) + Math.Pow(RightB, 2) - Math.Pow(RightC, 2)) / (2 * RightA * RightB));

            LeftAngle = (LeftAngle * 180) / Math.PI;
            RightAngle = (RightAngle * 180) / Math.PI;

            
                NewHipCenterAverage = (skeleton.Joints[JointType.HipLeft].Position.Y + skeleton.Joints[JointType.HipCenter].Position.Y + skeleton.Joints[JointType.HipRight].Position.Y) / 3;
                if (NewHipCenterAverage - JumpSegment1.HipCenterAverage > 0.05 && skeleton.Joints[JointType.Head].Position.Y - JumpSegment1.HeadY > 0.05)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
           
        }
    }
}
