using System;

namespace DefaultNamespace
{
    public class PointsHandler
    {
        private int _currentPoints;
        
        public int CurrentPoints
        {
            get => _currentPoints;
            set
            {
                int oldValue = _currentPoints;
                _currentPoints = value;
                if (oldValue != _currentPoints)
                {
                    OnPointsChanged?.Invoke(_currentPoints);
                }
            }
        }

        public event Action<int> OnPointsChanged;
    }
}