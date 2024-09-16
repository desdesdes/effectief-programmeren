Public Module SqlEscape
  public function LikeEscape(s as string) as string
    return s.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")
  end function
End Module
