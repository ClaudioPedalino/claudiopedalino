﻿using System;

namespace TotvsChallengePoC.Core.Helpers
{
    public static class GuidExtension
    {
        public static bool ValidateGuid(this Guid guid, string id)
        {
            Guid g = Guid.NewGuid();
            return Guid.TryParse(id, out g);
        }
    }
}
