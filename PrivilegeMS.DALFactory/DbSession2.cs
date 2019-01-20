 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivilegeMS.IDAL;
using PrivilegeMS.Model;
using System.Data.Entity;

namespace PrivilegeMS.DALFactory
{
	public partial class DBSession : IDBSession
    {
	
		private IActionInfoDal _ActionInfoDal;
        public IActionInfoDal ActionInfoDal
        {
            get
            {
                if(_ActionInfoDal == null)
                {
                    _ActionInfoDal = AbstractFactory.createActionInfoDal();
                }
                return _ActionInfoDal;
            }
            set { _ActionInfoDal = value; }
        }
	
		private IRoleInfoDal _RoleInfoDal;
        public IRoleInfoDal RoleInfoDal
        {
            get
            {
                if(_RoleInfoDal == null)
                {
                    _RoleInfoDal = AbstractFactory.createRoleInfoDal();
                }
                return _RoleInfoDal;
            }
            set { _RoleInfoDal = value; }
        }
	
		private IRUserInfoActionInfoDal _RUserInfoActionInfoDal;
        public IRUserInfoActionInfoDal RUserInfoActionInfoDal
        {
            get
            {
                if(_RUserInfoActionInfoDal == null)
                {
                    _RUserInfoActionInfoDal = AbstractFactory.createRUserInfoActionInfoDal();
                }
                return _RUserInfoActionInfoDal;
            }
            set { _RUserInfoActionInfoDal = value; }
        }
	
		private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if(_UserInfoDal == null)
                {
                    _UserInfoDal = AbstractFactory.createUserInfoDal();
                }
                return _UserInfoDal;
            }
            set { _UserInfoDal = value; }
        }
	}	
}