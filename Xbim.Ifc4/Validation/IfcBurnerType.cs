using System;
using Microsoft.Extensions.Logging;
using Xbim.Common;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Xbim.Common.Enumerations;
using Xbim.Common.ExpressValidation;
using Xbim.Ifc4.Interfaces;
// ReSharper disable once CheckNamespace
// ReSharper disable InconsistentNaming
namespace Xbim.Ifc4.HvacDomain
{
	public partial class IfcBurnerType : IExpressValidatable
	{
		public enum IfcBurnerTypeClause
		{
			CorrectPredefinedType,
		}

		/// <summary>
		/// Tests the express where-clause specified in param 'clause'
		/// </summary>
		/// <param name="clause">The express clause to test</param>
		/// <returns>true if the clause is satisfied.</returns>
		public bool ValidateClause(IfcBurnerTypeClause clause) {
			var retVal = false;
			try
			{
				switch (clause)
				{
					case IfcBurnerTypeClause.CorrectPredefinedType:
						retVal = (PredefinedType != IfcBurnerTypeEnum.USERDEFINED) || ((PredefinedType == IfcBurnerTypeEnum.USERDEFINED) && Functions.EXISTS(this/* as IfcElementType*/.ElementType));
						break;
				}
			} catch (Exception ) {
				/*var log = ApplicationLogging.CreateLogger<Xbim.Ifc4.HvacDomain.IfcBurnerType>();
				log.LogError(string.Format("Exception thrown evaluating where-clause 'IfcBurnerType.{0}' for #{1}.", clause,EntityLabel), ex);*/
			}
			return retVal;
		}

		public override IEnumerable<ValidationResult> Validate()
		{
			foreach (var value in base.Validate())
			{
				yield return value;
			}
			if (!ValidateClause(IfcBurnerTypeClause.CorrectPredefinedType))
				yield return new ValidationResult() { Item = this, IssueSource = "IfcBurnerType.CorrectPredefinedType", IssueType = ValidationFlags.EntityWhereClauses };
		}
	}
}