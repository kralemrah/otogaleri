﻿using Data.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Abstract
{
	public interface IService<T> : IRepository<T> where T : class ,IEntity, new()
	{

	}
}
