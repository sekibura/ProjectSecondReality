using NUnit.Framework;
using Altom.AltUnityDriver;

public class TestUIMainMenu
{
    public AltUnityDriver altUnityDriver;
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        altUnityDriver =new AltUnityDriver();
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altUnityDriver.Stop();
    }

    [Test]
    public void TestMenuButton()
    {
        altUnityDriver.LoadScene("Menu");
        altUnityDriver.FindObject(By.NAME, "ButtonMenu").Click();
        var enableMenu = altUnityDriver.FindObject(By.NAME, "MenuView").enabled;
        Assert.AreEqual(true, enableMenu);
    }

    [Test]
    public void TestQRMenuButton()
    {
        altUnityDriver.LoadScene("Menu");
        altUnityDriver.WaitForObject(By.NAME, "ButtonMenu", timeout:5);
        altUnityDriver.FindObject(By.NAME, "ButtonMenu").Click();
        altUnityDriver.FindObject(By.NAME, "ButtonQR").Click();
        var value = altUnityDriver.WaitForObject(By.NAME, "QRView", timeout: 5).enabled;
        Assert.AreEqual(true, value);
    }

    [Test]
    public void TestLeftMenuButtonOpen()
    {
        altUnityDriver.LoadScene("Menu");
        altUnityDriver.WaitForObject(By.NAME, "ButtonMenu", timeout: 5).Click();
        altUnityDriver.FindObject(By.NAME, "ButtonLeftMenu").Click();
        var leftEnable = altUnityDriver.FindObject(By.NAME, "LeftMenu").enabled;
        var menuEnable = altUnityDriver.FindObject(By.NAME, "MenuView").enabled;
        Assert.AreEqual(true, leftEnable);
        Assert.AreEqual(true, menuEnable);
    }

    [Test]
    public void TestLeftMenuButtonClose()
    {
        altUnityDriver.LoadScene("Menu");
        altUnityDriver.WaitForObject(By.NAME, "ButtonMenu", timeout: 5).Click();
        altUnityDriver.FindObject(By.NAME, "ButtonLeftMenu").Click();
        altUnityDriver.FindObject(By.NAME, "SmoothBackGround").Click();
        altUnityDriver.WaitForObjectNotBePresent(By.NAME, "LeftMenu", interval:3);
    }

    [Test]
    public void TestSettingsButtonOpen()
    {
        altUnityDriver.LoadScene("Menu");
        altUnityDriver.WaitForObject(By.NAME, "ButtonMenu", timeout: 5).Click();
        altUnityDriver.FindObject(By.NAME, "ButtonLeftMenu").Click();
        altUnityDriver.FindObject(By.NAME, "ButtonLMSettings").Click();
        var actual = altUnityDriver.FindObject(By.NAME, "SettingsView").enabled;
        Assert.AreEqual(true, actual);
    }

    [Test]
    public void TestSettingsButtonClose()
    {
        altUnityDriver.LoadScene("Menu");
        altUnityDriver.WaitForObject(By.NAME, "ButtonMenu", timeout: 5).Click();
        altUnityDriver.FindObject(By.NAME, "ButtonLeftMenu").Click();
        altUnityDriver.FindObject(By.NAME, "ButtonLMSettings").Click();
        altUnityDriver.FindObject(By.NAME, "ButtonSettingsBack").Click();
        altUnityDriver.WaitForObjectNotBePresent(By.NAME, "SettingsView", interval: 3);
    }

    [Test]
    public void TestAboutProgrammButtonOpen()
    {
        altUnityDriver.LoadScene("Menu");
        altUnityDriver.WaitForObject(By.NAME, "ButtonMenu", timeout: 5).Click();
        altUnityDriver.FindObject(By.NAME, "ButtonLeftMenu").Click();
        altUnityDriver.FindObject(By.NAME, "ButtonLMAboutProgramm").Click();
        var actual = altUnityDriver.FindObject(By.NAME, "AboutUsView").enabled;
        Assert.AreEqual(true, actual);
    }

    [Test]
    public void TestAboutProgrammButtonClose()
    {
        altUnityDriver.LoadScene("Menu");
        altUnityDriver.WaitForObject(By.NAME, "ButtonMenu", timeout: 5).Click();
        altUnityDriver.FindObject(By.NAME, "ButtonLeftMenu").Click();
        altUnityDriver.FindObject(By.NAME, "ButtonLMAboutProgramm").Click();
        altUnityDriver.FindObject(By.NAME, "ButtonAboutPrBack").Click();
        altUnityDriver.WaitForObjectNotBePresent(By.NAME, "AboutUsView", interval: 3);
    }

    [Test]
    public void TestQRScannerOpen()
    {
        altUnityDriver.LoadScene("Menu");
        altUnityDriver.WaitForObject(By.NAME, "ButtonStartQRScanner").Click();
        altUnityDriver.WaitForObject(By.NAME, "QRScannerView", timeout: 5).Click();
        var actual = altUnityDriver.FindObject(By.NAME, "QRScannerView").enabled;
        Assert.AreEqual(true, actual);
    }
}