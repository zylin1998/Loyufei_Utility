using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace Loyufei.MVP
{
    public class ModelPresenter<TModel> : Presenter
    {
        public TModel Model { get; private set; }

        [Inject]
        protected virtual void Construct(TModel model) 
        {
            Model = model;

            Init();
        }

        protected virtual void Init() 
        {

        }
    }
}