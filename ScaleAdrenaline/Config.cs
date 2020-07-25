using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleAdrenaline
{
    public class Config: IConfig
    {
        [Description("Whether or not the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        public float TimeAllSteps { get; set; } = 1.0f;

        public float[] Step1 { get; set; } = { 0.8f, 0.6f, 0.4f, 0.3f};

        public float[] Step2 { get; set; } = { 0.3f, 0.4f, 0.6f, 0.8f, 1.0f, 1.2f, 1.4f, 1.6f, 1.8f, 2.0f };

        public float[] Step3 { get; set; } = { 2.0f, 1.8f, 1.6f, 1.4f, 1.2f, 1.0f };

        public float Duration { get; set; } = 10.0f;

        public int MaxCountAdrenaline { get; set; } = 3;
    }
}
