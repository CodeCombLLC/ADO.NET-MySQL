// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See License.txt in the project root for license information.

using System;

namespace zlib
{
	
	sealed class zlibConst
	{
		private const System.String version_Renamed_Field = "1.0.2";
		public static System.String version()
		{
			return version_Renamed_Field;
		}
		
		// compression levels
		public const int Z_NO_COMPRESSION = 0;
		public const int Z_BEST_SPEED = 1;
		public const int Z_BEST_COMPRESSION = 9;
		public const int Z_DEFAULT_COMPRESSION = (- 1);
		
		// compression strategy
		public const int Z_FILTERED = 1;
		public const int Z_HUFFMAN_ONLY = 2;
		public const int Z_DEFAULT_STRATEGY = 0;
		
		public const int Z_NO_FLUSH = 0;
		public const int Z_PARTIAL_FLUSH = 1;
		public const int Z_SYNC_FLUSH = 2;
		public const int Z_FULL_FLUSH = 3;
		public const int Z_FINISH = 4;
		
		public const int Z_OK = 0;
		public const int Z_STREAM_END = 1;
		public const int Z_NEED_DICT = 2;
		public const int Z_ERRNO = - 1;
		public const int Z_STREAM_ERROR = - 2;
		public const int Z_DATA_ERROR = - 3;
		public const int Z_MEM_ERROR = - 4;
		public const int Z_BUF_ERROR = - 5;
		public const int Z_VERSION_ERROR = - 6;
	}
}