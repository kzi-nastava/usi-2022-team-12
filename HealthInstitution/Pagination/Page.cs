using System.Collections.Generic;
using HealthInstitution.Model;

namespace HealthInstitution.Pagination
{
    public class Page<BaseObservableEntity> where BaseObservableEntity : class
    {
        public IEnumerable<BaseObservableEntity> Entities;
        public int TotalElements { get; set; }
        public int Count { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public int Size { get; set; }
        public bool IsFirst => PageNumber == 0;
        public bool IsLast => PageCount == 0 || PageNumber == PageCount - 1;
    }
}
