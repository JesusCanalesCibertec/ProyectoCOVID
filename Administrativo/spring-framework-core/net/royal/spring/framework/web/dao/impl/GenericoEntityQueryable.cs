using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Utilities;
using System.ComponentModel;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Remotion.Linq;

namespace net.royal.spring.framework.web.dao.impl
{
    public class GenericoEntityQueryable<T> : EntityQueryable<T>, GenericoIQueryable<T>
    {
        public GenericoEntityQueryable(IAsyncQueryProvider provider)
            : base(provider)
        {
        }

        public GenericoEntityQueryable(IAsyncQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }
    }
}
