terraform {
  required_providers {
    cloudflare = {
      source = "cloudflare/cloudflare"
      version = "~>2.11.0"
    }
  }

  required_version = "~>0.13.0"
}
