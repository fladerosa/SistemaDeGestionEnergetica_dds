using SGE.Entidades.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace SGE.Entidades.Repositorio {
    public class BaseRepositorio<T> : IRepositorio<T> where T : class {
        public BaseRepositorio() {
            this.context = new SGEContext();
        }

        public BaseRepositorio(SGEContext contexto) {
            this.context = contexto;
        }

        //private SGEContext context {
        //    get {
        //        //return SGEContext.instancia();
        //        return new SGEContext();
        //    }
        //}

        private SGEContext context { get; set; }

        public List<T> GetAll() {
            context.Configuration.LazyLoadingEnabled = false;
            //TODO: se saca el lazy load ya que falla al momento de cargar los dispositivos inteligentes.
            //TODO: hay que revisar como hacer que convivan lazy load con un único contexto
            List<T> entidad = (List<T>)context.Set<T>().ToList();
            return entidad;
        }

        public List<T> GetAll(List<Expression<Func<T, object>>> includes) {
            List<string> includelist = new List<string>();

            foreach (var item in includes) {
                MemberExpression body = item.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("The body must be a member expression");

                includelist.Add(body.Member.Name);
            }

            DbQuery<T> query = context.Set<T>();

            includelist.ForEach(x => query = query.Include(x));

            return (List<T>)query.ToList();
        }

        public T Single(Expression<Func<T, bool>> predicate) {
            context.Configuration.LazyLoadingEnabled = false;

            T entidad = context.Set<T>().FirstOrDefault(predicate);
            return entidad;
        }

        public T Single(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes) {
            List<string> includelist = new List<string>();

            foreach (var item in includes) {
                MemberExpression body = item.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("The body must be a member expression");

                includelist.Add(body.Member.Name);
            }

            DbQuery<T> query = context.Set<T>();

            includelist.ForEach(x => query = query.Include(x));

            return query.FirstOrDefault(predicate);
        }

        public List<T> Filter(Expression<Func<T, bool>> predicate) {
            return (List<T>)context.Set<T>().Where(predicate).ToList();
        }

        public List<T> Filter(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes) {
            List<string> includelist = new List<string>();

            foreach (var item in includes) {
                MemberExpression body = item.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("The body must be a member expression");

                includelist.Add(body.Member.Name);
            }

            DbQuery<T> query = context.Set<T>();

            includelist.ForEach(x => query = query.Include(x));

            return (List<T>)query.Where(predicate).ToList();
        }

        public void Create(T entity) {
            context.Set<T>().Add(entity);
            // context.SaveChanges();
            try {
                context.SaveChanges();
            } catch (DbEntityValidationException ex) {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public void Update(T entity) {
            context.Entry(entity).State = EntityState.Modified;
            try {
                context.SaveChanges();
            } catch (DbEntityValidationException ex) {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public void Delete(T entity) {
            context.Entry(entity).State = EntityState.Deleted;
            try {
                context.SaveChanges();
            } catch (DbEntityValidationException ex) {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            } catch (Exception ex) {
                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", ex.Message);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage);
            }
        }

        public void Delete(Expression<Func<T, bool>> predicate) {
            var entities = context.Set<T>().Where(predicate).ToList();
            entities.ForEach(x => context.Entry(x).State = EntityState.Deleted);
            context.SaveChanges();
        }

    }
}

