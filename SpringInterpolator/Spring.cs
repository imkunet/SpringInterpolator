using System;
using System.Numerics;

// Code written by Kyle "KuNet" Nguyen

namespace SpringInterpolator
{
    // a one dimensional spring class
    public class Spring
    {
        // this value is used to calculate the delta in time between each physics update
        private DateTime _lastTime = DateTime.Now;

        // the current position of the end of the spring
        private float _current;
        
        // the current motion of the spring
        private float _motion;
        
        // controls the anchor of the spring (where the cursor will spring to)
        public float Target { get; set; }

        // the settings which govern the spring (thanks discord for helping me out on this one)
        private readonly SpringSettings _settings;
        
        public Spring(SpringSettings settings)
        {
            _settings = settings;
        }

        // idk if there is a built in function to do this in C# so...
        private static bool Compare(float a, float b)
        {
            return MathF.Abs(a - b) < 0.000f;
        }
        
        // called every time the current position of the spring needs to be retrieved
        // it will consider the current time and automatically compensate should the calls be at an abnormal rate
        public float Update()
        {
            var currentTime = DateTime.Now;
            var deltaTime = currentTime - _lastTime;

            // performance
            if (deltaTime.TotalMilliseconds == 0) return _current;

            // more optimization
            if (Compare(_current, Target) || Compare(_motion, 0.0f))
            {
                _current = Target;
                _motion = 0.0f;
                return _current;
            }

            // milliseconds might be too small to make the spring effect visible so the divide variable is there
            var delta = (float) deltaTime.TotalMilliseconds / _settings.Divide;
            // since the spring is updating, we can update the last time the spring was updated
            _lastTime = currentTime;

            // add the acceleration and multiply it with the distance between the target and current to go
            // towards the target and multiply it by the delta so it is correct time-wise
            _motion += _settings.Acceleration * (Target - _current) * delta;
            
            // dampen the motion so that the spring does not oscillate forever (is really funny when is 0)
            // might be a feature to remove this in the feature ???? lol
            _motion *= MathF.Pow(_settings.Dampening, delta);
            
            // applies the motion to the current position and correct for time difference
            _current += _motion * delta;

            // supply the current position
            return _current;
        }
    }

    // to be honest this could have all been done without this (using vector math)
    public class Spring2D
    {
        // represent the x, y vector of the spring end
        private readonly Spring _x;
        private readonly Spring _y;
        
        public Spring2D(SpringSettings springSettings)
        {
            _x = new Spring(springSettings);
            _y = new Spring(springSettings);
        }

        // same as the Spring#update function
        public Vector2 Update()
        {
            return new(_x.Update(), _y.Update());
        }

        // updates the target of the spring
        public void UpdateTarget(Vector2 target)
        {
            _x.Target = target.X;
            _y.Target = target.Y;
        }
    }
    
    // holds the acceleration, dampening, and divide values
    // this one is for all you oxford comma haters >:)
    public class SpringSettings
    {
        public SpringSettings(float acceleration, float dampening, float divide)
        {
            Acceleration = acceleration;
            Dampening = dampening;
            Divide = divide;
        }
        
        public float Acceleration { get; set; }
        public float Dampening { get; set; }
        public float Divide { get; set; }
    }
}
