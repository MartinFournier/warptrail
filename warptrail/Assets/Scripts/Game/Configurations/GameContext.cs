using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;

namespace trailapp.Game
{
    public class GameContext : MVCSContext
    {
        public GameContext(MonoBehaviour view)
            : base(view)
        {
        }

        public GameContext(MonoBehaviour view, ContextStartupFlags flags)
            : base(view, flags)
        {
        }

        // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        // Override Start so that we can fire the StartSignal
        override public IContext Start()
        {
            base.Start();

            var startSignal = injectionBinder.GetInstance<GameStartSignal>();
            startSignal.Dispatch();

            return this;
        }

        protected override void mapBindings()
        {
            //Models
            //          injectionBinder.Bind<ILevel> ().To<Level> ().ToSingleton ();

            //Commands & Signals
            //          commandBinder.Bind<GameStartSignal> ().To<GameStartCommand> ();

            //Singleton signals
            injectionBinder.Bind<GameStartSignal>().ToSingleton();

            //Mediation
            //          mediationBinder.Bind<LevelSceneView> ().To<LevelSceneMediator> ();

            //Services
            //          injectionBinder.Bind<ILevelGenerator> ().To<SpecificLevelGenerator> ();
            //          injectionBinder.Bind<INodePositionServices> ().To<NodePositionServices> ().ToSingleton ();
        }

        protected override void postBindings()
        {
            var context = (contextView as GameObject);
            var cam = context.GetComponentInChildren<UnityEngine.Camera>();
            injectionBinder.Bind<UnityEngine.Camera>().ToValue(cam);

            //          var configs = context.GetComponentInChildren<Configs> ();
            //          injectionBinder.Bind<Configs> ().ToValue (configs);
        }
    }
}

