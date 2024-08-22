using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MSTestUnit
{
    [TestClass]
    public class UnitTest1
    {
        int tiempoEspera = 300;
        private readonly IWebDriver driver;
        public UnitTest1()
        {
            driver = new ChromeDriver();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        /*
        [TestMethod]
        
        public void Create_Get_ReturnCreateView()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:7085/ClientSql/Create");
            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Id("Provincia")).SendKeys("Pichincha");
            Thread.Sleep(tiempoEspera);
            driver.FindElement(By.Id("Apellidos")).SendKeys("Pozo");
            Thread.Sleep(tiempoEspera);
            driver.FindElement(By.Id("Nombres")).SendKeys("Steven");
            Thread.Sleep(tiempoEspera);
            driver.FindElement(By.Id("FechaNacimiento")).SendKeys("28/01/1980 0:00:00");
            Thread.Sleep(tiempoEspera);
            driver.FindElement(By.Id("Mail")).SendKeys("steven@espe.edu.ec");
            Thread.Sleep(tiempoEspera);
            driver.FindElement(By.Id("Telefono")).SendKeys("0963901669");
            Thread.Sleep(tiempoEspera);
            driver.FindElement(By.Id("Direccion")).SendKeys("Tumbaco");
            Thread.Sleep(tiempoEspera);
            driver.FindElement(By.Id("Estado")).SendKeys("True");
            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Id("btnUwU")).Click();
            driver.Quit();

        }
        */
        /*
        [TestMethod]
        public void EditClient()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:7085/ClientSql/Edit/1");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // Espera a que el campo "Direccion" esté visible y actúa en él
            var direccionField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Direccion")));
            direccionField.Clear();
            direccionField.SendKeys("Fondo de bikini");
            Thread.Sleep(tiempoEspera);


            // Espera a que el botón "btnEdit" sea clickeable
            var btnEdit = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnEdit")));

            // Desplaza el elemento a la vista
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", btnEdit);

            // Intentar hacer clic usando JavaScript si el clic normal falla
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", btnEdit);

            driver.Quit();
        }
        */
        /*
        [TestMethod]
        public void DeleteClient()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:7085/ClientSql/Delete/28");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // Espera a que el botón "btnDelete" sea clickeable
            var btnDelete = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnDelete")));
            Thread.Sleep(tiempoEspera);

            // Desplaza el botón a la vista
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", btnDelete);

            // Intenta hacer clic usando JavaScript si el clic normal falla
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", btnDelete);

            driver.Quit();
        }
        */

    }
}