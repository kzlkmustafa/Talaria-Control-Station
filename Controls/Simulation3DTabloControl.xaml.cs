using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Talaria.Models;

namespace Talaria
{
    /// <summary>
    /// Interaction logic for Simulation3DTabloControl.xaml
    /// </summary>
    public partial class Simulation3DTabloControl : UserControl
    {

        private AxisAngleRotation3D rotationX;
        private AxisAngleRotation3D rotationY;
        private AxisAngleRotation3D rotationZ;
        public Simulation3DTabloControl()
        {
            InitializeComponent();


            Transform3DGroup transformGroup = new Transform3DGroup();

            rotationX = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);
            rotationY = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
            rotationZ = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

            transformGroup.Children.Add(new RotateTransform3D(rotationX));
            transformGroup.Children.Add(new RotateTransform3D(rotationY));
            transformGroup.Children.Add(new RotateTransform3D(rotationZ));

            // Load the 3D model
            var importer = new ModelImporter();
            Model3D model = importer.Load("../../../objModels/uydu.obj");

            // Center the model
            Rect3D bounds = model.Bounds;
            Vector3D offset = new Vector3D(
                -(bounds.X + bounds.SizeX / 2),
                -(bounds.Y + bounds.SizeY / 2),
                -(bounds.Z + bounds.SizeZ / 2)
            );

            Transform3DGroup centeringGroup = new Transform3DGroup();
            centeringGroup.Children.Add(new TranslateTransform3D(offset));
            centeringGroup.Children.Add(transformGroup);
            model.Transform = centeringGroup;

            // Calculate camera position
            double maxDimension = Math.Max(bounds.SizeX, Math.Max(bounds.SizeY, bounds.SizeZ));
            double cameraDistance = maxDimension * 2;

            // Set camera position
            var camera = new PerspectiveCamera
            {
                Position = new Point3D(0, 0, cameraDistance),
                LookDirection = new Vector3D(0, 0, -cameraDistance),
                UpDirection = new Vector3D(0, 1, 0)
            };
            helixViewport.Camera = camera;

            // Add the model to the viewport
            helixViewport.Children.Add(new ModelVisual3D { Content = model });

        }


        public myRotationThreeD threeDTabloData
        {
            get { return (myRotationThreeD)GetValue(threeDataProperty); }
            set
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(() => SetValue(threeDataProperty, value));
                }
                else
                {
                    SetValue(threeDataProperty, value);
                }
            }
        }

        public static readonly DependencyProperty threeDataProperty =
            DependencyProperty.Register("PersonData", typeof(myRotationThreeD), typeof(Simulation3DTabloControl), new PropertyMetadata(null, OnThreeChanged));

        private static void OnThreeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Simulation3DTabloControl control)
            {
                var info = e.NewValue as myRotationThreeD;
                control.Dispatcher.Invoke(() =>
                {
                    control.OnRotate(info.roll, info.pitch, info.yaw);
                });
            }
        }



        private void OnRotate(float myroll, float mypitch, float myyaw)
        {
            
                rotationX.Angle = (double)myroll;
                rotationY.Angle = (double)mypitch;
                rotationZ.Angle = (double)myyaw;
            

            rollValueTxt.Text = myroll.ToString();
            pitchValueTxt.Text= mypitch.ToString();
            yawValueTxt.Text= myyaw.ToString();

        }
    }
}
