module "azure" {
  source = "./modules/azure"

  environment_definition = var.azure_environment_definition
  kubernetes_cluster     = var.azure_kubernetes_cluster
  public_ip              = var.azure_public_ip
  resource_group         = var.azure_resource_group
  storage_account        = var.azure_storage_account
  storage_container      = var.azure_storage_container
  storage_share          = var.azure_storage_share
}

module "cloudflare" {
  source = "./modules/cloudflare"

  record_name  = var.cloudflare_record_name
  record_value = module.azure.fqdn
  zone_name    = var.cloudflare_zone_name
}

terraform {
  backend "remote" {}
}
