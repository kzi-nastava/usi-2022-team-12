using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions
{
    public class RecommendationNotFoundException : Exception
    {
        public RecommendationNotFoundException() { }

        public RecommendationNotFoundException(string message) : base(message) { }
    }
}
