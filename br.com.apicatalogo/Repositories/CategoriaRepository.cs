using System;
using System.Linq.Expressions;
using br.com.apicatalogo.Context;
using br.com.apicatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace br.com.apicatalogo.Repositories
{
	public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
	{
		public CategoriaRepository(ApiCatalogoContext context) : base(context)
		{

		}

	}
}

