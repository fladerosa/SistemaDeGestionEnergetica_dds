using SGE.Entidades.Categorias;
using SGE.Entidades.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Repositorio.RepositorioCategoria
{
    public class CategoriaRepositorio : BaseRepositorio<Categorias.Categoria>, ICategoriaRepositorio
    {
        public Categoria GetById(int CategoriaId)
        {
            using (SGEContext context = new SGEContext())
            {
                return context.Set<Categoria>().FirstOrDefault(x => x.CategoriaId == CategoriaId);
            }

        }
    }
}
