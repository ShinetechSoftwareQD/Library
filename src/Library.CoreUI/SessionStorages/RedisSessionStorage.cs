﻿using ServiceStack.Redis;

namespace Library.CoreUI.SessionStorages
{
	public class RedisSessionStorage : ISessionStorage
	{
		private RedisClient _redisClient = null;

		public RedisSessionStorage(string url, int port)
		{
			_redisClient = new RedisClient(url, port);
		}

		public void Dispose()
		{
			_redisClient = null;
		}

		public T Get<T>(string key)
		{
			return _redisClient.Get<T>(key);
		}

		public void Set<T>(string key, T value)
		{
			_redisClient.Add<T>(key, value);
		}
	}
}