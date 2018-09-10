﻿using pg.im.installer;
using RSG;
using UnityEngine;

using pg.im.model;

namespace pg.im.view
{
    public partial class StartupMediator
    {
        public class StartupStateLoadStaticData : StartupState
        {
            private readonly LoadStaticDataSignal _loadStaticDataSignal;

            public StartupStateLoadStaticData(StartupMediator mediator) : base(mediator)
            {
                _loadStaticDataSignal = mediator._loadStaticDataSignal;
            }

            public override void OnStateEnter()
            {
                base.OnStateEnter();

                Promise staticDataPromise = new Promise();
                _loadStaticDataSignal.Fire(staticDataPromise);

                staticDataPromise.Then(
                    () => {
                        StartupModel.LoadingProgress.Value = StartupModel.ELoadingProgress.StaticDataLoaded;
                    }
                ).Catch(e =>
                {
                    Debug.LogError("Exception seeding static data 2: " + e.ToString());
                });
            }
        }
    }
}