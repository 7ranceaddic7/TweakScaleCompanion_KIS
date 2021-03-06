﻿/*
	This file is part of TweakScaleCompanion_KIS
	© 2020 LisiasT : http://lisias.net <support@lisias.net>

	TweakScaleCompanion_KIS is double licensed, as follows:

	* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
	* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	TweakScaleCompanion_KIS is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with TweakScaleCompanion_KIS. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with TweakScaleCompanion_KIS. If not, see <https://www.gnu.org/licenses/>.

*/
using System.Linq;
using UnityEngine;

namespace TweakScaleCompanion.KIS
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	internal class Startup : MonoBehaviour
	{
		private void Start()
		{
			Log.init();
			Log.force("Version {0}", Version.Text);

			try
			{
				KSPe.Util.Installation.Check<Startup>(System.IO.Path.Combine("TweakScaleCompanion", "KIS"));
			}
			catch (KSPe.Util.InstallmentException e)
			{
				Log.error(e.ToShortMessage());
				KSPe.Common.Dialogs.ShowStopperAlertBox.Show(e);
			}

			this.checkDependencies();
		}

		private void checkDependencies()
		{
			AssemblyLoader.LoadedAssembly assembly = AssemblyLoader.loadedAssemblies.Where(a => a.assembly.GetName().Name == "TweakScale").First();
			if (-1 == assembly.assembly.GetName().Version.CompareTo(new System.Version(2, 4, 4)) )
				GUI.UnmetRequirementsShowStopperAlertBox.Show("TweakScale v2.4.4 or superior");
		}
	}
}
