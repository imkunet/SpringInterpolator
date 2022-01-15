using System.Numerics;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Output;
using OpenTabletDriver.Plugin.Tablet;

namespace SpringInterpolator
{
    [PluginName("SpringInterpolator")]
    public class SpringInterpolator : AsyncPositionedPipelineElement<IDeviceReport>
    {
        private readonly SpringSettings _settings;
        // future idea: make the z springy (pressure)
        private readonly Spring2D _spring;
        private uint pressure;

        public SpringInterpolator()
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
        
        [SliderProperty("Speed", 1f, 5000f, 40f), DefaultPropertyValue(40f), ToolTip(
             "This value controls the speed at which the spring simulation runs\n" +
             "Higher -> More \"sluggish\" (Default: 40.0)")
        ]
        public float Divide
        {
            get => _settings.Divide;
            set => _settings.Divide = value;
        }

        protected override void UpdateState()
        {
            if (State is ITabletReport _report)
            {
                _report.Position = _spring.Update();
                _report.Pressure = this.pressure;
                State = _report;
            }

            if (PenIsInRange())
            {
                OnEmit();
            }
        }

        protected override void ConsumeState()
        {
            if (State is ITabletReport _report)
            {
                _spring.UpdateTarget(new Vector2(_report.Position.X, _report.Position.Y));
                this.pressure = _report.Pressure;
            }
        }
        public override PipelinePosition Position => PipelinePosition.PreTransform;
    }
}