using OpenGl3d;
using OpenGl3d.Infrastructure.Shapes;
using OpenTK.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Camera _camera;
        OpenGlHandler _handler;
        HashSet<Key> _pressed;
        bool _moveMouse = false;

        IReadOnlyDictionary<Key, Action<double?>> _keyMap;

        public MainWindow()
        {
            InitializeComponent();
            _camera = new ();
            _handler = new (new TetrahedronWrapper(), _camera);
            _pressed = new ();
            _keyMap = new Dictionary<Key, Action<double?>>()
            {
                [Key.W] = _camera.Forward,
                [Key.A] = _camera.Left,
                [Key.S] = _camera.Backwards,
                [Key.D] = _camera.Right,
                [Key.LeftCtrl] = _camera.Down,
                [Key.LeftShift] = _camera.Up
            };

            var settings = new GLWpfControlSettings();

            OpenTkControl.Render += OpenTkControl_Render;
            OpenTkControl.Start(settings);

            _handler.OnLoad();
            KeyDown += MainWindow_KeyDown;
            KeyUp += MainWindow_KeyUp;

            OpenTkControl.MouseLeftButtonDown += MainWindow_MouseDown;
            OpenTkControl.MouseLeftButtonUp += MainWindow_MouseLeftButtonUp;
        }

        private void MainWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            OpenTkControl.ReleaseMouseCapture();

            _moveMouse = false;
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.None;
            OpenTkControl.CaptureMouse();
            _moveMouse = true;

            _camera.ResetMouse();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (_keyMap.ContainsKey(e.Key))
            {
                _pressed.Remove(e.Key);
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (_keyMap.ContainsKey(e.Key))
            {
                _pressed.Add(e.Key);
            }
        }

        private void OpenTkControl_Render(TimeSpan obj)
        {
            void HandleCamera(Key key, Action<double?> cameraAction)
            {
                if (_pressed.Contains(key))
                {
                    cameraAction(obj.TotalSeconds);
                }
            }
               
            foreach (var (key, action) in _keyMap)
            {
                HandleCamera(key, action);
            }


            if (_moveMouse)
            {
                var mouse = Mouse.GetPosition(OpenTkControl);
                _camera.MouseMove((float)mouse.X, (float)mouse.Y);
            }

            _handler.OnRenderFrame();
        }

        private void OnSetCube_Click(object sender, RoutedEventArgs e)
        {
            _handler.SetShape(new CubeWrapper());
        }

        private void SetTetrahedron_Click(object sender, RoutedEventArgs e)
        {
            _handler.SetShape(new TetrahedronWrapper());
        }

        private void SetOctahedron_Click(object sender, RoutedEventArgs e)
        {
            _handler.SetShape(new OctahedronWrapper());
        }

        private void SetWired_Checked(object sender, RoutedEventArgs e)
        {
            _handler.SetWired(true);
        }

        private void SetWired_Unchecked(object sender, RoutedEventArgs e)
        {
            _handler.SetWired(false);
        }
    }
}
