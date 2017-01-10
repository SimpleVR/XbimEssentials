using System;
using log4net;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Xbim.Common.Enumerations;
using Xbim.Common.ExpressValidation;
using Xbim.Ifc4.Interfaces;
using static Xbim.Ifc4.Functions;
// ReSharper disable once CheckNamespace
// ReSharper disable InconsistentNaming
namespace Xbim.Ifc4.HvacDomain
{
	public partial class IfcMedicalDeviceType : IExpressValidatable
	{
		public enum IfcMedicalDeviceTypeClause
		{
			CorrectPredefinedType,
		}

		/// <summary>
		/// Tests the express where-clause specified in param 'clause'
		/// </summary>
		/// <param name="clause">The express clause to test</param>
		/// <returns>true if the clause is satisfied.</returns>
		public bool ValidateClause(IfcMedicalDeviceTypeClause clause) {
			var retVal = false;
			try
			{
				switch (clause)
				{
					case IfcMedicalDeviceTypeClause.CorrectPredefinedType:
						retVal = (PredefinedType != IfcMedicalDeviceTypeEnum.USERDEFINED) || ((PredefinedType == IfcMedicalDeviceTypeEnum.USERDEFINED) && EXISTS(this/* as IfcElementType*/.ElementType));
						break;
				}
			} catch (Exception ex) {
				var Log = LogManager.GetLogger("Xbim.Ifc4.HvacDomain.IfcMedicalDeviceType");
				Log.Error(string.Format("Exception thrown evaluating where-clause 'IfcMedicalDeviceType.{0}' for #{1}.", clause,EntityLabel), ex);
			}
			return retVal;
		}

		public override IEnumerable<ValidationResult> Validate()
		{
			foreach (var value in base.Validate())
			{
				yield return value;
			}
			if (!ValidateClause(IfcMedicalDeviceTypeClause.CorrectPredefinedType))
				yield return new ValidationResult() { Item = this, IssueSource = "IfcMedicalDeviceType.CorrectPredefinedType", IssueType = ValidationFlags.EntityWhereClauses };
		}
	}
}