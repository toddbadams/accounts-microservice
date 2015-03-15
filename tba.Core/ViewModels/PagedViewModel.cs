using System.Collections.Generic;
using System.Linq;
using tba.Core.Entities;

namespace tba.Core.ViewModels
{
    public class PagedViewModel<TM, TE> 
        where TM : ReadOnlyViewModel
        where TE : Entity
    {
        public int Start { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public TM[] List { get; set; }

        public static PagedViewModel<TM, TE> Create(int start, int total, IEnumerable<TE> list) 
        {
            var entities = list as TE[] ?? list.ToArray();
           // var models = TM.From(entities);
            return new PagedViewModel<TM, TE>
            {
                Start = start,
                Total = total,
                Limit = entities.Count(),
                List = null // models.ToArray()
            };
        }
    }
}