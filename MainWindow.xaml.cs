using AForge.Video.DirectShow;
using AForge.Video;
using System.Windows.Threading;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Definitions.Charts;
using Talaria.Models;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMap.NET;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Talaria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Rotation3DValueModel myrotation3DValueModel {  get; set; }

        private AxisAngleRotation3D rotationX;
        private AxisAngleRotation3D rotationY;
        private AxisAngleRotation3D rotationZ;
        public MainWindow()
        {
            InitializeComponent();

            myrotation3DValueModel = new Rotation3DValueModel
            {
                roll = "0",
                pitch = "0",
                yaw = "0",
            };

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
            OnRotate(myrotation3DValueModel);
        }
        private void OnRotate(Rotation3DValueModel rotation3D)
        {
            if (double.TryParse(rotation3D.roll, out double x) &&
                double.TryParse(rotation3D.pitch, out double y) &&
                double.TryParse(rotation3D.yaw, out double z))
            {
                rotationX.Angle = x;
                rotationY.Angle = y;
                rotationZ.Angle = z;
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values for rotations.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


    }
}