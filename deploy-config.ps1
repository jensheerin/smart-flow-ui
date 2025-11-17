# Configuration Variables
$resourceGroup = "rg-smartflow-dev"
$location = "eastus2"  # Or: westus, westus3, swedencentral
$uniqueSuffix = "jci01"  # Use your initials + number for uniqueness

# Resource names (must be globally unique for some services)
$openAIName = "openai-smartflow-$uniqueSuffix"
$searchName = "search-smartflow-$uniqueSuffix"
$cosmosName = "cosmos-smartflow-$uniqueSuffix"
$storageName = "stsmartflow$uniqueSuffix"  # No hyphens, lowercase only

# Save for later use
$configFile = "deployment-outputs.txt"