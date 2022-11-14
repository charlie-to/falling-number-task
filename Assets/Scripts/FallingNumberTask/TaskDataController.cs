using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion.Client;
using FallingNumberTask.Shared.Services;
using UnityEngine;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace FallingNumberTask
{
    public class TaskDataController : MonoBehaviour
    {
        private GrpcChannel _channel;
        private ITaskDataService _service;

        void Awake()
        {
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var _channel = GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions
            {
                HttpHandler = new HttpClientHandler()
            });
            _service = MagicOnionClient.Create<ITaskDataService>(_channel);
            Debug.Log("connected");
        }

        async void Start()
        {
            var x = UnityEngine.Random.Range(0, 1000);
            var y = UnityEngine.Random.Range(0, 1000);
            var result = await _service.SumAsync(x, y);
            Debug.Log($"Result: {result}");
        }

        async void OnDestroy()
        {
            if (_channel != null)
            {
                await _channel.ShutdownAsync();
            }
        }
    }
}
