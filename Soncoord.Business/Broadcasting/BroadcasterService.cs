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
            _rotationTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromSeconds(10) // ToDo: Configurable or fix to 10
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
                    Console.WriteLine($"Authentication failed.");
                    return;
                }
                catch (ErrorResponseException ex)
                {
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
            return _webSocket.ListScenes();
        }

        public void SetScene(string sceneName)
        {
            _webSocket.SetCurrentScene(sceneName);
        }

        public void StartRandomSceneRoation()
        {
            RotationScenes = GetScenes().Where(scene => scene.Name.StartsWith("Cam")).ToList();
            SetSceneRandomly();
            _rotationTimer.Start();
        }

        public void StopRandomSceneRotation()
        {
            _rotationTimer.Stop();
            SetScene("Moderation");
        }

        private void RotationTimerTick(object sender, EventArgs e)
        {
            SetSceneRandomly();
        }

        private void SetSceneRandomly()
        {
            var currentScene = _webSocket.GetCurrentScene().Name;

            if (currentScene == "Moderation")
            {
                _rotationTimer.Interval = TimeSpan.FromSeconds(1);
                return;
            }
            else if (currentScene == "GO")
            {
                _rotationTimer.Interval = TimeSpan.FromSeconds(10);
            }

            SetScene(RotationScenes[_random.Next(RotationScenes.Count)].Name);
        }
    }
}
