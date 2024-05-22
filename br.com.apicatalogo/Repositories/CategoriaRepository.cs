using br.com.apicatalogo.Context;
using br.com.apicatalogo.Models;

namespace br.com.apicatalogo.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApiCatalogoContext context) : base(context)
        {

        }

    }
}

