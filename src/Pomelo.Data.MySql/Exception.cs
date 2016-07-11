// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using System;
using System.Data.Common;
#if NET451
using System.Runtime.Serialization;
#endif

namespace Pomelo.Data.MySql
{
    /// <summary>
    /// The exception that is thrown when MySQL returns an error. This class cannot be inherited.
    /// </summary>
    /// <include file='docs/MySqlException.xml' path='MyDocs/MyMembers[@name="Class"]/*'/>
#if NET451
    
#endif
  public sealed class MySqlException : DbException
  {
    private int errorCode;
    private bool isFatal;

    internal MySqlException()
    {
    }

    internal MySqlException(string msg)
      : base(msg)
    {
    }

    internal MySqlException(string msg, Exception ex)
      : base(msg, ex)
    {
    }

    internal MySqlException(string msg, bool isFatal, Exception inner)
      : base(msg, inner)
    {
      this.isFatal = isFatal;
    }

    internal MySqlException(string msg, int errno, Exception inner)
      : this(msg, inner)
    {
      errorCode = errno;
      Data.Add("Server Error Code", errno);
    }

    internal MySqlException(string msg, int errno)
      : this(msg, errno, null)
    {
    }

#if NET451
    private MySqlException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
#endif

        /// <summary>
        /// Gets a number that identifies the type of error.
        /// </summary>
        public int Number
    {
      get { return errorCode; }
    }

        

    /// <summary>
    /// True if this exception was fatal and cause the closing of the connection, false otherwise.
    /// </summary>
    internal bool IsFatal
    {
      get { return isFatal; }
    }

    internal bool IsQueryAborted
    {
      get
      {
        return (errorCode == (int)MySqlErrorCode.QueryInterrupted ||
          errorCode == (int)MySqlErrorCode.FileSortAborted);
      }
    }
  }
}
