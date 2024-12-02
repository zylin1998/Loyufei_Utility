using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Loyufei.MVP;

namespace Loyufei
{
    public class InputManagePresenter<TModel> : ModelPresenter<TModel> where TModel : InputManageModel
    {
        protected override void RegisterEvents()
        {
            Register<ChangeInput>(Change);
            Register<ResetInput> (Reset);
        }

        private void Change(ChangeInput change) 
        {
            var origin = Model.GetOrigin(change.StorageId, change.InputId, change.Positive);
            var key    = EInputKey.None;

            Observable
                .EveryUpdate()
                .TakeWhile((l) => Model.GetInput(origin, out key))
                .Subscribe(
                (l) => { },
                ()  =>
                {
                    Model.Change(change.StorageId, change.InputId, key, change.Positive);

                    SettleEvents(new UpdateInput(change.StorageId, change.InputId, key, change.Positive));

                    OnEnd();
                });
        }

        private void Reset(ResetInput reset) 
        {
            Model.Reset(reset.StorageId);
        }

        protected virtual void OnEnd() 
        {

        }
    }
}
