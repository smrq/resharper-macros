using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Macros;
using JetBrains.ReSharper.Feature.Services.Lookup;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services;
using JetBrains.Util;

namespace ResharperMacros
{
	[Macro("ResharperMacros.MethodParameterMacro",
		ShortDescription = "Parameters of containing method",
		LongDescription = "Obtains the parameters of the containing method it is used in")]
	public class MethodParameterMacro : IMacro
	{
		public ParameterInfo[] Parameters { get { return EmptyArray<ParameterInfo>.Instance; } }

		public HotspotItems GetLookupItems(IHotspotContext context, IList<string> arguments)
		{
			if (context.SessionContext.TextControl == null)
				return null;

			var container = TextControlToPsi.GetContainingTypeOrTypeMember(
				context.SessionContext.Solution,
				context.SessionContext.TextControl);

			if (!(container is IMethod))
				return null;
			var method = (IMethod) container;

			var lookupItems = new List<ILookupItem>();
			lookupItems.AddRange(method.Parameters.Select(param => new TextLookupItem(param.ShortName)));

			return new HotspotItems(lookupItems);
		}

		public string EvaluateQuickResult(IHotspotContext context, IList<string> arguments)
		{
			return null;
		}

		public string GetPlaceholder(IDocument document)
		{
			return "a";
		}

		public bool HandleExpansion(IHotspotContext context, IList<string> arguments)
		{
			return false;
		}
	}
}
