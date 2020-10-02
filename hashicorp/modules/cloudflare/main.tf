data "cloudflare_zones" "rvtr" {
  filter {
    name   = var.zone_name
    paused = false
    status = "active"
  }
}

resource "cloudflare_record" "rvtr" {
  name    = var.record_name
  proxied = false
  ttl     = 1
  type    = "CNAME"
  value   = var.record_value
  zone_id = lookup(data.cloudflare_zones.rvtr.zones[0], "id")
}
