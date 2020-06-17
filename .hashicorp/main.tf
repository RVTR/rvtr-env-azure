module "azure" {
  source = "./modules/azure"

  kubernetes_cluster = var.azure_kubernetes_cluster
  resource_group     = var.azure_resource_group
}

module "cloudflare" {
  source = "./modules/cloudflare"

  record = var.cloudflare_record
  zones  = var.cloudflare_zones
}

terraform {
  backend "local" {}
}
