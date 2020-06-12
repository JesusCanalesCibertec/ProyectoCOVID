using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Internal;

namespace net.royal.spring.framework.web.dao
{
    public interface GenericoIQueryable<out T> : IQueryable<T>
    {
    }
}
