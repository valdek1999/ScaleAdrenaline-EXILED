using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Exiled.API.Extensions;
using Exiled.API.Interfaces;
using Exiled.Events;
using Handlers = Exiled.Events.Handlers;

namespace ScaleAdrenaline
{
    public class Plugin: Exiled.API.Features.Plugin<Config>
    {
		public override string Author { get; } = "HardFoxy";
		public override string Name { get; } = "ScaleAdrenaline";
		public override string Prefix { get; } = "ScaleInjector";
		public override Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;
		public override Version RequiredExiledVersion { get; } = new Version(2, 0, 7);

		public EventHandler EventHandler;
		public override void OnEnabled()
		{
			try
			{
				Log.Info("привееееееет");
				EventHandler = new EventHandler(this);
				base.OnEnabled();
				RegisterEvents();
			}
			catch (Exception e)
			{
				Log.Error($"Loading error: {e}");
			}
		}
		internal void RegisterEvents()
		{
			Handlers.Player.MedicalItemUsed += EventHandler.UsedAdrenaline;
		}
		public override void OnDisabled()
		{
			Handlers.Player.MedicalItemUsed -= EventHandler.UsedAdrenaline;
		}

		public override void OnReloaded()
		{

		}


	}
}
