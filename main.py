from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.chrome.service import Service as ChromeService
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import TimeoutException

def get_final_url(initial_url):
    # Configure Chrome options for headless mode
    chrome_options = Options()
    chrome_options.add_argument("--headless")

    # Set up Chrome WebDriver
    driver = webdriver.Chrome(service=ChromeService(ChromeDriverManager().install()), options=chrome_options)

    try:
        # Open the initial URL
        driver.get(initial_url)

        # Wait until certain elements are present indicating that content has loaded
        wait = WebDriverWait(driver, 10)
        final_url_element = wait.until(EC.visibility_of_element_located((By.CSS_SELECTOR, "article")))
        final_url = driver.current_url
        return final_url
    except TimeoutException:
        print("Timed out waiting for page to load")
        return None
    finally:
        # Close the WebDriver session
        driver.quit()

# Example usage:
initial_url = f"https://news.google.com/read/CBMi0gFBVV95cUxPTjE2aXIxX0lTUUdGRUxGS0cwR25HTzhxRTdfRVA2WWtLTmtKN1V5VXE1ekRmOWNzeTYzcUlKMGtKVDlESGdKNjlLQVd2Q19pblZUbmhpQmMtY1duRVd5NkViTnFpNjFGUW5jNndZb0k5OXEtN21qd05hOEV6cDV5RWtUS1N1ZXlvUTVjTkRuMVZONTVzazRUWXRndXdVUmtKVlItUWROenFXSkowSFpiZGkzQi1ZQmU1OEdsRnZMbFBWZlFzSXJfV2xFLS1nS0JBUVHSAeABQVVfeXFMTThZSnNmTk9Vblp0MTM0aUlQQm1TTUpTLUxyMFdwUkF3RkZGTU9fQ1hMWXY5SWpsdlo5YlVwVTFzdmJodHJRX0Jhb2pja2ZEWmN5STZWRlhtNHlPNUNqMXJfRkhGWGoyU1huVWdUZ1FWMldqd291c1c4Uzh0VEhxWHV5Rkk3S3A2WFVyU1VpV3hIbHo1NVZwd3dubzdjZ1piX1FHamRQUEUxTERFaVhIeHVwMTl1MExHMTNnVFFTZkRDSklPZnZwLTlzNmF0Zlo0VnpoTXF3N0daalZXNnR5RmM?hl=pt-BR&gl=BR&ceid=BR%3Apt-419"
final_url = get_final_url(initial_url)
if final_url:
    print("Final URL after content loaded:", final_url)