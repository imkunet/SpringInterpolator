using System.Numerics;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet.Interpolator;
using OpenTabletDriver.Plugin.Timers;

namespace SpringInterpolator
{
    [PluginName("SpringInterpolator")]
    public class SpringInterpolator : Interpolator
    {
        private readonly SpringSettings _settings;
        // future idea: make the z springy (pressure)
        private readonly Spring2D _spring;
        private SyntheticTabletReport _report;

        public SpringInterpolator(ITimer scheduler) : base(scheduler)
        {
            // improvement/tweak: make the default target the current position of the cursor
            _settings = new SpringSettings(0.4f, 0.3f, 100f);
            _spring = new Spring2D(_settings);
        }
        
        [SliderProperty("Acceleration", 0.0001f, 0.9999f, 0.9f), DefaultPropertyValue(0.9f),
        ToolTip("Controls the acceleration of the motion of the spring towards the target\n" +
                "Higher -> Faster (Default 0.9)")]
        public float Acceleration
        {
            get => _settings.Acceleration;
            set => _settings.Acceleration = value;
        }
        
        [SliderProperty("Dampening", 0.0001f, 0.9999f, 0.3f), DefaultPropertyValue(0.3f),
        ToolTip("Dampening controls what the motion of the spring is multiplied by each physics tick\n" +
                "Higher -> Less dampening (Default: 0.3)")]
        public float Dampening
        {
            get => _settings.Dampening;
            set => _settings.Dampening = value;
        }
        
        [SliderProperty("Speed", 1, 5000, 40), DefaultPropertyValue(40), ToolTip(
             "This value controls the speed at which the spring simulation runs\n" +
             "Higher -> More \"sluggish\" (Default: 40)")
        ]
        public float Divide
        {
            get => _settings.Divide;
            set => _settings.Divide = value;
        }

        public override SyntheticTabletReport Interpolate()
        {
            _report.Position = _spring.Update();
            return _report;
        }

        public override void UpdateState(SyntheticTabletReport report)
        {
            _spring.UpdateTarget(new Vector2(report.Position.X, report.Position.Y));
            _report = report;
        }
    }
}