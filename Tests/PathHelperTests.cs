using LuaEditor.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class PathHelperTests
    {
        [TestMethod]
        public void RelativePathForFilenameOnly()
        {
            string filePath = @"player.lua";
            string rootPath = @"E:\Source\TileMore";

            string expected = @"player.lua";

            string actual = PathHelper.ToRelativePath(filePath, rootPath);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RelativePathForSameFolder()
        {
            string filePath = @"E:\Source\TileMore\player.lua";
            string rootPath = @"E:\Source\TileMore";

            string expected = @"player.lua";
            string actual = PathHelper.ToRelativePath(filePath, rootPath);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RelativePathForChildFolder()
        {
            string filePath = @"E:\Source\TileMore\Includes\player.lua";
            string rootPath = @"E:\Source\TileMore";

            string expected = @"Includes\player.lua";
            string actual = PathHelper.ToRelativePath(filePath, rootPath);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RelativePathForParentFolder()
        {
            string filePath = @"E:\Source\TileMore\TileMore.proj";
            string rootPath = @"E:\Source\TileMore\Includes";

            string expected = @"..\TileMore.proj";
            string actual = PathHelper.ToRelativePath(filePath, rootPath);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AbsolutePathForSameFolder()
        {
            string filePath = @"player.lua";
            string rootPath = @"E:\Source\TileMore";

            string expected = @"E:\Source\TileMore\player.lua";
            string actual = PathHelper.ToAbsolutePath(filePath, rootPath);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AbsolutePathForParentFolder()
        {
            string filePath = @"..\TileMore.proj";
            string rootPath = @"E:\Source\TileMore\Includes";

            string expected = @"E:\Source\TileMore\TileMore.proj";
            string actual = PathHelper.ToAbsolutePath(filePath, rootPath);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AbsolutePathForChildFolder()
        {
            string filePath = @"Includes\player.lua";
            string rootPath = @"E:\Source\TileMore";

            string expected = @"E:\Source\TileMore\Includes\player.lua";
            string actual = PathHelper.ToAbsolutePath(filePath, rootPath);

            Assert.AreEqual(expected, actual);
        }
    }
}