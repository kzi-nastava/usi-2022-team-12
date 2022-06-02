﻿using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
{
    public class HealthInstitutionSurveyCommand : CommandBase
    {
        public HealthInstitutionSurveyCommand()
        {

        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("HealthInstitutionSurvey");
        }
    }
}