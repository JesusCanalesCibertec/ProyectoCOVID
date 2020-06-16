using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

namespace net.royal.spring.framework.web.dao.impl
{
    public class GenericoInternalDbSet<TEntity> : InternalDbSet<TEntity>, GenericoIQueryable<TEntity> where TEntity : class
    {
        public GenericoInternalDbSet(DbContext context)
            : base(context)
        {
        }
    }
}
