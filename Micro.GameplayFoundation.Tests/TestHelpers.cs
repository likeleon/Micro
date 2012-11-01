using System.IO;

namespace Micro.GameplayFoundation.Tests
{
    public static class TestHelpers
    {
        private static string SolutionRootPath
        {
            get { return Path.GetFullPath(@"..\..\..\..\"); }
        }

        public static string TestAssetsPath
        {
            get { return Path.Combine(SolutionRootPath, @"Assets\Test\"); }
        }

        public static string TestAsset
        {
            get { return @"TestFile.file"; }
        }

        public static string EngineCoreAssetsPath
        {
            get { return Path.Combine(SolutionRootPath, @"Assets\EngineCore\"); }
        }
    }
}
