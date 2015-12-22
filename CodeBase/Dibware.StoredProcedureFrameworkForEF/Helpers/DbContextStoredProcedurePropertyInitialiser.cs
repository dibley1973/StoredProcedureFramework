using System;
using System.Data.Entity;
using System.Reflection;

namespace Dibware.StoredProcedureFrameworkForEF.Helpers
{
    internal class DbContextStoredProcedurePropertyInitialiser
    {
        private readonly DbContext _context;
        private readonly Type _contextType;
        private readonly PropertyInfo _property;
        private readonly string _propertyName;
        private readonly Type _propertyType;
        private ConstructorInfo _constructorInfo;
        private object _procedure;

        public DbContextStoredProcedurePropertyInitialiser(
            DbContext context, PropertyInfo storedProcedurePropertyInfo)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (storedProcedurePropertyInfo == null) throw new ArgumentNullException("storedProcedurePropertyInfo");

            _context = context;
            _contextType = _context.GetType();
            _property = storedProcedurePropertyInfo;
            _propertyName = _property.Name;
            _propertyType = _property.PropertyType;

            BuildConstructorInfo();
            EnsureConstructor();
            BuildProcedure();
            EnsureProcedure();
        }

        private void BuildConstructorInfo()
        {
            _constructorInfo = _propertyType.GetConstructor(new[] { _contextType });
        }

        private void EnsureConstructor()
        {
            if (_constructorInfo == null) throw new InvalidOperationException(
                "A public non parametricised constructor must be available for a stored " +
                "procedure which is a property of the DbContext. " + _propertyName +
                " does not appear to have this. ");
        }

        private void BuildProcedure()
        {
            _procedure = _constructorInfo.Invoke(new object[] { _context });
        }

        private void EnsureProcedure()
        {
            if (_procedure == null) throw new InvalidOperationException(
                "Stored procedure '" + _propertyName + "' could not be instantiated");
        }

    }
}
