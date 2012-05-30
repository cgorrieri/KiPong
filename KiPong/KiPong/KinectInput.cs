using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Kinect;
using System.Threading;

namespace KiPong
{
    public enum KinectState { OK, PENDING, NO }

    public class KinectInput : Input
    {
        private KiPongGame game;

        const int skeletonCount = 2;
        Skeleton[] allSkeletons = new Skeleton[skeletonCount];
        KinectSensor kinectSensor;
        Skeleton playerOne, playerTwo;

        /// <summary>
        /// Etats des positions précédentes
        /// </summary>
        private bool lastBack, lastEnter, lastAide;

        /// <summary>
        /// Si la kinect est connectée
        /// </summary>
        private bool Ready
        {
            get { return (kinectSensor != null && kinectSensor.Status == KinectStatus.Connected); }
        }

        /// <summary>
        /// Si un joueur est détecté
        /// </summary>
        public bool ReadyForOne
        { get { return (Ready && playerOne != null); } }

        /// <summary>
        /// Si deux joueur sont détectés
        /// </summary>
        public bool ReadyForTwo
        { get { return (ReadyForOne && playerTwo != null); } }

        /// <summary>
        /// Position de la main droite du joueur 1
        /// </summary>
        public int LeftY { get; set; }
        /// <summary>
        /// Position de la main droite du joueur 2
        /// </summary>
        public int RightY { get; set; }

        public KinectInput(KiPongGame g)
        {
            game = g;
            LeftY = RightY = 0;
            KinectSensor.KinectSensors.StatusChanged += new EventHandler<StatusChangedEventArgs>(KinectSensors_StatusChanged);
            DiscoverKinectSensor();
        }

        public override void UnloadContent()
        {
            if (kinectSensor != null)
            {
                // On reset l'angle de la kinect 
                kinectSensor.ElevationAngle = 0;
                kinectSensor.Stop();
                kinectSensor.Dispose();
            }
        }

        #region Kinect
        void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            if (this.kinectSensor == e.Sensor)
            {
                if (e.Status == KinectStatus.Disconnected ||
                    e.Status == KinectStatus.NotPowered)
                {
                    this.kinectSensor = null;
                    this.DiscoverKinectSensor();
                }
            }
        }

        private void DiscoverKinectSensor()
        {
            foreach (KinectSensor sensor in KinectSensor.KinectSensors)
            {
                if (sensor.Status == KinectStatus.Connected)
                {
                    // Found one, set our sensor to this
                    kinectSensor = sensor;
                    break;
                }
            }

            if (this.kinectSensor == null)
            {
                return;
            }

            // Init the found and connected device
            if (kinectSensor.Status == KinectStatus.Connected)
            {
                InitializeKinect();
            }
        }

        private bool InitializeKinect()
        {
            kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            //kinectSensor.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(kinectSensor_ColorFrameReady);
            // Skeleton Stream
            kinectSensor.SkeletonStream.Enable(new TransformSmoothParameters()
            {
                Smoothing = 0.9f,
                Correction = 0.1f,
                Prediction = 0.1f,
                JitterRadius = 0.05f,
                MaxDeviationRadius = 0.05f
            });
            kinectSensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinectSensor_SkeletonFrameReady);
            try
            {
                kinectSensor.Start();
            }
            catch
            {
                return false;
            }
            return true;
        }

        void kinectSensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    Skeleton[] skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];

                    skeletonFrame.CopySkeletonDataTo(skeletonData);
                    List<Skeleton> players = (from s in skeletonData where s.TrackingState == SkeletonTrackingState.Tracked select s).ToList<Skeleton>();
                    if (players.Count == 0)
                    {
                        playerOne = playerTwo = null;
                    }
                    else
                    {
                        playerOne = players[0];
                        if (players.Count > 1)
                            playerTwo = players[1];
                        else
                            playerTwo = null;
                    }
                    if (playerOne != null)
                    {
                        LeftY = UpdateHandsPosition(playerOne);
                    }
                    if (playerTwo != null)
                    {
                        RightY = UpdateHandsPosition(playerTwo);
                        // Single le joueur deux est a gauche on inverse les deux joueurs
                        if (playerOne.Joints[JointType.Spine].Position.X > playerTwo.Joints[JointType.Spine].Position.X)
                        {
                            Skeleton tmp = playerOne;
                            playerOne = playerTwo;
                            playerTwo = tmp;
                            int tmpI = LeftY;
                            LeftY = RightY;
                            RightY = tmpI;
                        }
                    }
                }
            }
        }
        #endregion

        #region Calcul de Position

        private int UpdateHandsPosition(Skeleton sk)
        {
            float rightHandY = sk.Joints[JointType.HandRight].Position.Y;
            float BoundTopY = sk.Joints[JointType.Head].Position.Y;
            float BoundBottomY = sk.Joints[JointType.HipCenter].Position.Y;

            if (BoundTopY < rightHandY) rightHandY = BoundTopY;
            float ratio = (BoundTopY - rightHandY) / (BoundTopY - BoundBottomY);
            return (int)(game.ScreenHeight * ratio);
        }

        #endregion

        public override bool Back()
        {
            if (ReadyForOne)
            {
                bool now = playerOne.Joints[JointType.HandRight].Position.X < playerOne.Joints[JointType.Spine].Position.X;
                bool result = now && !lastBack;
                lastBack = now;
                return result;
            }
            return false;
        }

        public override bool Valid()
        {
            if (ReadyForOne)
            {
                bool now = playerOne.Joints[JointType.HandLeft].Position.X > playerOne.Joints[JointType.Spine].Position.X;
                bool result = now && !lastEnter;
                lastEnter = now;
                return result;
                
            }
            return false;
        }

        public override bool Help()
        {
            if (ReadyForOne)
            {
                bool now = playerOne.Joints[JointType.HandLeft].Position.Y > playerOne.Joints[JointType.Head].Position.Y;
                bool result = now && !lastAide;
                lastAide = now;
                return result;
            }
            return false;
        }

        public override bool Break()
        {
            return Valid();
        }
    }
}
