data "cloudflare_zones" "rvtr" {
  filter {
    name   = var.zones.name
    paused = false
    status = "active"
  }
}

resource "cloudflare_record" "rvtr" {
  name    = var.record.name
  proxied = false
  ttl     = 1
  type    = var.record.type
  value   = var.record.value
  zone_id = lookup(data.cloudflare_zones.rvtr.zones[0], "id")
}
