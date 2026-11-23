using System.Collections.Generic;
using AquariumGame.Features.PointerInput.Scripts.Services.Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace AquariumGame.Features.PointerInput.Scripts.Services
{
    public sealed class PointerInputService : IPointerInputService, ITickable
    {
        private readonly List<Entry> _handlers = new List<Entry>();
        private readonly List<ITapHandler> _snapshot = new List<ITapHandler>();

        public void Register(ITapHandler handler, int priority)
        {
            if (handler == null)
                return;

            Unregister(handler);

            var index = _handlers.FindIndex(entry => entry.Priority < priority);
            var item = new Entry(handler, priority);

            if (index < 0)
                _handlers.Add(item);
            else
                _handlers.Insert(index, item);
        }

        public void Unregister(ITapHandler handler) => _handlers.RemoveAll(entry => entry.Handler == handler);

        public void Tick()
        {
            if (_handlers.Count == 0 || Time.timeScale <= 0f)
                return;

            var pointer = Pointer.current;
            if (pointer == null || !pointer.press.wasPressedThisFrame)
                return;

            var eventSystem = EventSystem.current;
            if (eventSystem != null && eventSystem.IsPointerOverGameObject())
                return;

            var camera = Camera.main;
            if (camera == null)
                return;

            Vector2 worldPoint = camera.ScreenToWorldPoint(pointer.position.ReadValue());

            _snapshot.Clear();
            foreach (var entry in _handlers)
                _snapshot.Add(entry.Handler);

            foreach (var handler in _snapshot)
                if (handler.TryHandleTap(worldPoint))
                    break;
        }

        private readonly struct Entry
        {
            public readonly ITapHandler Handler;
            public readonly int Priority;

            public Entry(ITapHandler handler, int priority)
            {
                Handler = handler;
                Priority = priority;
            }
        }
    }
}
