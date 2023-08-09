﻿using CRM.DataAccess.Abstract;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.DataAccess.EntityFramework
{
	public class EFOrderRepo : GenericRepo<Order>, IOrderDal
	{
	}
}
