using System;

namespace HealthInstitution.Core.Exceptions
{
    public class RecommendationNotFoundException : Exception
    {
        public RecommendationNotFoundException() { }

        public RecommendationNotFoundException(string message) : base(message) { }
    }
}
