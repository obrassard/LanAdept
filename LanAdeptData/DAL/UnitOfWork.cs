﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LanAdeptData.DAL.Generic;
using LanAdeptData.DAL.Users;

namespace LanAdeptData.DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		private LanAdeptDataContext context;

		public static UnitOfWork Current
		{
			get { return HttpContext.Current.Items["_UnitOfWork"] as UnitOfWork; }
		}

		public UnitOfWork()
		{
			context = new LanAdeptDataContext();
		}

		private UserRepository userRepository;
		public UserRepository UserRepository
		{
			get
			{
				if (userRepository == null)
					userRepository = new UserRepository(context);

				return userRepository;
			}
		}

		private RoleRepository roleRepository;
		public RoleRepository RoleRepository
		{
			get
			{
				if (roleRepository == null)
					roleRepository = new RoleRepository(context);

				return roleRepository;
			}
		}

		private PermissionRepository permissionRepository;
		public PermissionRepository PermissionRepository
		{
			get
			{
				if (permissionRepository == null)
					permissionRepository = new PermissionRepository(context);

				return permissionRepository;
			}
		}

		private LoginHistoryRepository loginHistoryRepository;
		public LoginHistoryRepository LoginHistoryRepository
		{
			get
			{
				if (loginHistoryRepository == null)
					loginHistoryRepository = new LoginHistoryRepository(context);

				return loginHistoryRepository;
			}
		}

		public void Save()
		{
			context.SaveChanges();
		}

		#region IDisposable

		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

	}
}