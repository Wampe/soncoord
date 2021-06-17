using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Soncoord.Business.Broadcasting
{
    public class BroadcasterService
    {
        private readonly Random _random;
        private readonly DispatcherTimer _rotationTimer;
        private readonly OBSWebsocket _webSocket;
        
        public BroadcasterService()
        {
            _webSocket = new OBSWebsocket();
            _rotationTimer = new DispatcherTimer(DispatcherPriority.Send)
            {
                Interval = TimeSpan.FromSeconds(3) // ToDo: Configurable or fix to 10
            };
            _rotationTimer.Tick += RotationTimerTick;
            _random = new Random();

            if (!_webSocket.IsConnected)
            {
                try
                {
                    _webSocket.Connect("ws://localhost:4444", null); // ToDo: Configurable
                }
                catch (AuthFailureException)
                {
                    //MessageBox.Show("Authentication failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Console.WriteLine($"Authentication failed.");
                    return;
                }
                catch (ErrorResponseException ex)
                {
                    //MessageBox.Show("Connect failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Console.WriteLine($"Connect failed: {ex.Message}");
                    return;
                }

            }
            else
            {
                _webSocket.Disconnect();
            }
        }

        private IList<OBSScene> RotationScenes { get; set; }

        public IList<OBSScene> GetScenes()
        {
            return _webSocket.GetSceneList().Scenes;
        }

        public void SetScene(string sceneName)
        {
            _webSocket.SetCurrentScene(sceneName);
            Console.WriteLine($"Current Scene: {sceneName}");
        }

        public void StartRandomSceneRoation()
        {
            RotationScenes = GetScenes();
            _rotationTimer.Start();
        }

        public void StopRandomSceneRotation()
        {
            _rotationTimer.Stop();
        }

        private void RotationTimerTick(object sender, EventArgs e)
        {
            SetScene(RotationScenes[_random.Next(RotationScenes.Count)].Name);
        }
    }
}
