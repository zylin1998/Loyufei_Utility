using System;
using System.Linq;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace Loyufei
{
    public class FileManagePresenter<TModel> : MVP.ModelPresenter<TModel> where TModel : FileManageModel
    {
        protected override void RegisterEvents()
        {
            Register<SaveData>(Save);
        }

        private void Save(SaveData save) 
        {
            Model.Save(save.Id);
        }
    }

    public struct SaveData : IDomainEvent 
    {
        public SaveData(object id) 
        {
            Id = id;
        }

        public object Id { get; }
    }
}
