﻿using System.Collections.Generic;
using System.Linq;

namespace SNR.Core.Utils
{
    public static class ListUtils
    {
        public static bool isEmpty(object list)
        {
            List<object> listObject = ((IEnumerable<object>)list).ToList();

            return listObject.Count == 0;

        }
    }
}